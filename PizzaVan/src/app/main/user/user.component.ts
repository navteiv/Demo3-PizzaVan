import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/core/services/data.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
  }
  loadData(){
    this.dataService.get('api/User')
  }
}
