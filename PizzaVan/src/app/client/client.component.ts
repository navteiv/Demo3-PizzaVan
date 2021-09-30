import { Component, OnInit } from '@angular/core';
import { AuthenticateService } from '../core/services/authenticate.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css'],
})
export class ClientComponent implements OnInit {
  checkAuthen!: boolean;
  fullName = localStorage.getItem("fullName");
  count = localStorage.getItem("countCartItem");

  constructor(private authen: AuthenticateService) {
  }
  ngOnInit(): void {
    console.log(this.fullName);

  }
  ngDoCheck(){
    this.checkAuthen = this.authen.isUserAuthenticate();
    this.fullName = localStorage.getItem("fullName");
    this.count = localStorage.getItem("countCartItem");
  }
  logOut(): void{
    this.authen.weblogout();
  }

}
