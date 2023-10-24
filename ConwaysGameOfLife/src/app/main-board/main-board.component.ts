import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { Cells } from '../cells';

@Component({
  selector: 'app-main-board',
  templateUrl: './main-board.component.html',
  styleUrls: ['./main-board.component.css']
})

export class MainBoardComponent {

  constructor(private dataService: DataService) {
  }

  cellModel: any;
  cellsContent: any;
  hasStarted: boolean = false;
  bReset: boolean = false;
  polling: any;
  timer = 0;
  dimension: any;
  
  ngOnInit() {
    this.getDimensionData();
  }
  
  ngOnDestroy() {
    if (this.polling) {
      clearInterval(this.polling);
    }
  }

  getData() {
    this.hasStarted = true;
    this.dataService.getData().subscribe(data => {
      this.cellModel = new Cells();
      Object.assign(this.cellModel, JSON.parse(data))
      console.log(data);
    });

    setTimeout(() => {
      this.displayCells();
    }, 1000);
  }

  getInitialData() {
    this.dataService.getInitialData().subscribe(data => {
      this.cellModel = new Cells();
      Object.assign(this.cellModel, JSON.parse(data))
      console.log(data);
    });

    setTimeout(() => {
      this.displayCells();
    }, 1000);
    
  }

  getDimensionData() {
    this.dataService.getDimensionData().subscribe(data => {
      this.dimension = data;
      console.log(this.dimension);
    });
  }

  start() {
    if (!this.hasStarted) {
      this.getDimensionData();
      this.getInitialData();
    }

    if (!this.polling)
      this.polling = setInterval(() => { this.getData() }, 1000);
  }

  stop() {
    if (this.polling) {
      clearInterval(this.polling);
      this.polling = 0;
    }
  }

  reset() {
    this.stop();
    setTimeout(() => {
      this.hasStarted = false;
      this.cellsContent = '';
    }, 1000);
  }

  displayCells() {
    let content: string = '';
    let i = 0;
    if (this.dimension.length > 0) {
      for (let x = 0; x < this.dimension[0]; x++) {
        
        for (let y = 0; y < this.dimension[1]; y++) {
          
          let str = this.cellModel[i].value;
          content += str + '&nbsp;&nbsp;';
          i++;

          if (y === this.dimension[1] - 1) {
            content += '</br>';
          }
        }
      }
      this.cellsContent = content.replaceAll('undefined', '')
        .replaceAll('1', 'o').replaceAll('0', '_');
    }
  }
}
