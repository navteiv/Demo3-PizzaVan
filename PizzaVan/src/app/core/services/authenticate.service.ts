import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SystemConstants } from '../common/constants';


@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {
  invalidLogin?: boolean;
  constructor(private router: Router, private http: HttpClient) { }

  login(username: string, password: string){
    // let headers = new HttpHeaders();
    const headers = new HttpHeaders();
    headers.append('Accept', '*/*');
    headers.append('Content-Type', 'application/json');
    return this.http.post(SystemConstants.LOCAL_API + 'api/Token/login', {username: username, password: password}, {headers: headers})
    .subscribe(response => {
      const token = (<any>response).token;
      const name = (<any>response).fullName
      console.log(token);
      localStorage.removeItem("jwt");
      localStorage.setItem("jwt", token);
      localStorage.setItem("fullName", name);
      this.invalidLogin = false;
      this.router.navigate(["/main"]);
    }, err => {
      this.invalidLogin = true;
      alert("Đăng nhập thất bại");
    });
  }
  webLogin(email: string, password: string){
    // let headers = new HttpHeaders();
    const headers = new HttpHeaders();
    headers.append('Accept', '*/*');
    headers.append('Content-Type', 'application/json');
    return this.http.post(SystemConstants.LOCAL_API + 'api/Token/weblogin', {email: email, password: password}, {headers: headers})
    .subscribe(response => {
      const token = (<any>response).token;
      const name = (<any>response).fullName;
      const cusId = (<any>response).id
      console.log(token);
      localStorage.removeItem("jwt");
      localStorage.setItem("jwt", token);
      localStorage.setItem("fullName", name);
      localStorage.setItem("cusId", cusId);
      this.invalidLogin = false;
      this.router.navigate(["/client"]);
    }, err => {
      this.invalidLogin = true;
      alert("Đăng nhập thất bại");
    });
  }
  logout(){
    localStorage.removeItem("jwt");
    this.router.navigate(["/login"]);
  }
  weblogout(){
    localStorage.removeItem("jwt");
    localStorage.removeItem("fullName");
    this.router.navigate(["/client"]);
  }
  isUserAuthenticate(): boolean{
    let token = localStorage.getItem("jwt");
    if(token != null){
      return true;
    }else return false;
  }
  getLoggedInUser(): any{
    return null;
  }
}
