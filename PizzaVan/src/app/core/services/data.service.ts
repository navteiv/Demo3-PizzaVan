import { HttpClient, HttpEventType, HttpHeaders } from '@angular/common/http';
import { Injectable, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { SystemConstants } from '../common/constants';
import { AuthenticateService } from './authenticate.service';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root',
})
export class DataService {
  private headers!: HttpHeaders;
  constructor(private router: Router, private http: HttpClient, private authen: AuthenticateService) {
    this.headers = new HttpHeaders();
  }
  get(uri: string) {
    this.headers.delete("Authorization");
    this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    return this.http.get(SystemConstants.LOCAL_API + uri, {headers: this.headers})
    .pipe(map((response: any) => response));
  }
  post(uri: string, data?: any) {
    // this.headers.delete("Authorization");
    // this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    const httpOptions = {
      authorization: new HttpHeaders({'Authorization' : 'Bearer' + localStorage.getItem("jwt")}),
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.post(SystemConstants.LOCAL_API + uri, data, httpOptions)
    .pipe(map((response: any) => response));
  }
  put(uri: string, data?: any) {
    // this.headers.delete("Authorization");
    // this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    const httpOptions = {
      authorization: new HttpHeaders({'Authorization' : 'Bearer' + localStorage.getItem("jwt")}),
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.put(SystemConstants.LOCAL_API + uri, data, httpOptions)
    .pipe(map((response: any) => response));
  }
  delete(uri: string) {
    // this.headers.delete("Authorization");
    // this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    const httpOptions = {
      authorization: new HttpHeaders({'Authorization' : 'Bearer' + localStorage.getItem("jwt")}),
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.delete(SystemConstants.LOCAL_API + uri, httpOptions)
    .pipe(map((response: any) => response));
  }
  public handleError(error: any){
    if(error.status == 401){
      localStorage.removeItem("jwt");
      alert("Mời bạn đăng nhập lại");
      this.router.navigate(["/main"]);
    }else{

    }
  }
}
