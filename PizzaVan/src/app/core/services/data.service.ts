import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SystemConstants } from '../common/constants';
import { AuthenticateService } from './authenticate.service';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root',
})
export class DataService {
  private headers!: HttpHeaders;
  constructor(private router: Router, private http: HttpClient, private authen: AuthenticateService) {}

  get(uri: string) {
    this.headers.delete("Authorization");
    this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    return this.http.get(SystemConstants.LOCAL_API + uri, {headers: this.headers})
    .pipe(map((response: any) => response.json()));
  }
  post(uri: string, data?: any) {
    this.headers.delete("Authorization");
    this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    return this.http.post(SystemConstants.LOCAL_API + uri, data, {headers: this.headers})
    .pipe(map((response: any) => response.json()));
  }
  put(uri: string, data?: any) {
    this.headers.delete("Authorization");
    this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    return this.http.put(SystemConstants.LOCAL_API + uri, data, {headers: this.headers})
    .pipe(map((response: any) => response.json()));
  }
  delete(uri: string, key: string, id: number) {
    this.headers.delete("Authorization");
    this.headers.append("Authorization", "Bearer" + localStorage.getItem("jwt"));
    return this.http.delete(SystemConstants.LOCAL_API + uri + "/?" + key + "=" + id, {headers: this.headers})
    .pipe(map((response: any) => response.json()));
  }
}
