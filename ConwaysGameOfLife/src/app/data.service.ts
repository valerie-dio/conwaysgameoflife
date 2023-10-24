import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cells } from './cells';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) {

  }

  getData() {
    return this.http.get("http://localhost:48104/api/Generation/Get", { responseType: 'text' });
  }

  getInitialData() {
    return this.http.get("http://localhost:48104/api/Generation/GetInitialState", { responseType: 'text' });
  }


  getDimensionData() {
    return this.http.get("http://localhost:48104/api/Generation/GetDimension",);
  }
}
