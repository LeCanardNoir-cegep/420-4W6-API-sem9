import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Front-end';
  data:any;

  constructor( private _http:HttpClient ) {
  }

  ngOnInit(): void {
    this._http.get<any>(`https://localhost:5001/api/Owners`).subscribe( res => this.data = res );
  }

  registerUser(){
    let user = {
      username:"Roland123",
      email:"roland@boomer.ca",
      password: "Password!2345",
      passwordConfirm: "Password!2345"
    };
    this._http.post<any>(`https://localhost:5001/api/Owners`, user).subscribe( res => this.data = res );
  }

}
