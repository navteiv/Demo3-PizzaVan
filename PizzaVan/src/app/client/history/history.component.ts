import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/core/services/data.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  public orders!: any[];
  id = localStorage.getItem("cusId");

  constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.loadData();
  }
  loadData(): void{
    this.dataService.get('api/Order/' + this.id).subscribe(
      (response: any) => {
        this.orders = response;
        console.log(this.orders);
      }
    )
  }
}
