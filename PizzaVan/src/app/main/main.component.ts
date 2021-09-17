import { Component, OnInit } from '@angular/core';
import { AuthenticateService } from '../core/services/authenticate.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(private authen: AuthenticateService) { }

  ngOnInit(): void {
  }
  logOut(): void{
    this.authen.logout();
  }
}
