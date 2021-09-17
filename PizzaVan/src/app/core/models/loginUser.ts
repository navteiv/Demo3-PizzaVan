export class LoginUser{
  constructor(access_token: string, username: string, fullname: string, email: string){
    this.access_token = access_token;
    this.username = username;
    this.fullname = fullname;
    this.email = email;
  }
  public id?: number;
  public access_token: string;
  public username: string;
  public fullname: string;
  public email: string;
}
