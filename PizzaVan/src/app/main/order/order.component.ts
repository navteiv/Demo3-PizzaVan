import { Component, OnInit } from '@angular/core';
import { ExcelExportData } from '@progress/kendo-angular-excel-export';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { orderBy, SortDescriptor } from '@progress/kendo-data-query';
import { DataService } from 'src/app/core/services/data.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css', '../../../assets/css/pdf-export.css']
})
export class OrderComponent implements OnInit {
  public pageSize = 5;
  public skip = 0;
  public loading!: boolean;
  public data!: any[];
  public gridView!: GridDataResult;

  public type = 'numeric';
  public buttonCount = 5;
  public info = true;
  public previousNext = true;
  public position = 'bottom';

  public sort: SortDescriptor[] = [
    {
      field: "orderId",
      dir: "desc",
    },
  ];
  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort;
    this.loadData();
  }
  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.loadData();
    this.allData = this.allData.bind(this);
  }
  loadData(){
    this.dataService.get('api/Order').subscribe(
      (response: any) => {
        this.data = response;
        this.gridView = {
          data: orderBy(this.data, this.sort).slice(this.skip, this.skip + this.pageSize),
          total: this.data.length
        }
      }
    )
  }
  public pageChange({skip, take}: PageChangeEvent): void {
    this.skip = skip;
    this.pageSize = take;
    this.loadData();
  }
  public allData(): ExcelExportData{
    const result: ExcelExportData = {
      data: this.data
    }
    return result;
  }
}
