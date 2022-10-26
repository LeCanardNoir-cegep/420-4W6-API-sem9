import {Component, Input, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Front-end';
  testToken:any;
  data:any;

  constructor( private _http:HttpClient ) {
  }

  ngOnInit(): void {
  }

  getCat(){
    let token = JSON.parse(localStorage.getItem("token")!);
    console.log("getCat",token.token === this.testToken)
    let httpOptions = {
      headers: new HttpHeaders({
        //'Content-Type' : 'application/json',
        'Authorization' : "Bearer " + token.token
      })
    }
    this._http.get<any>("https://localhost:5001/api/Cats", httpOptions).subscribe( x => {
      console.log("getCat respond:", x);
    } );
  }

  login(){
    const user = {
      username:"Roland123",
      password: "Password!2345"
    };
    this._http.post<any>(`https://localhost:5001/api/Owners/Login`, user).subscribe( res => {
      localStorage.setItem("token", JSON.stringify(res));
      this.testToken = res.token;
      console.log(res.token);
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
