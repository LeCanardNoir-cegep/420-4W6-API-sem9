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
  }

  login(){
    const user = {
      username:"Roland123",
      password: "Password!2345"
    };
    this._http.post<any>(`https://localhost:5001/api/Owners/Login`, user).subscribe( res => {
      localStorage.setItem("token", JSON.stringify(res));
      console.log(res);
    } );
  }

  registerUser(){
    let user = {
      username:"Roland123",
      email:"roland2@boomer.ca",
      password: "Password!2345",
      passwordConfirm: "Password!2345"
    };
    this._http.post<any>(`https://localhost:5001/api/Owners/Register`, user).subscribe( res => console.log(res) );
  }

}
