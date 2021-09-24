import { Component, Input, OnInit } from '@angular/core';
import { ExcelExportData } from '@progress/kendo-angular-excel-export';
import { DataStateChangeEvent, GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { orderBy, SortDescriptor, process, State } from '@progress/kendo-data-query';
import { DataService } from 'src/app/core/services/data.service';

@Component({
  selector: 'app-dish',
  templateUrl: './dish.component.html',
  styleUrls: ['./dish.component.css', "../../../assets/css/pdf-export.css"]
})

export class DishComponent implements OnInit {

  constructor(private dataService: DataService) { }
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

  public state: State = {
    skip: 0,
    take: 5,
    filter: {
      logic: "and",
      filters: [{field: "name", operator:"contains", value: ""}]
    }
  }
  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.gridView = process(this.data, this.state);

  }
  public sort: SortDescriptor[] = [
    {
      field: "dishId",
      dir: "asc",
    },
  ];
  ngOnInit(): void {
    this.loadData();
    this.allData = this.allData.bind(this);
  }

  loadData(): void{
    this.dataService.get('api/Dish').subscribe(
      (response: any) => {
        this.data = response;
        this.gridView = {
          data: orderBy(this.data, this.sort).slice(this.skip, this.skip + this.pageSize),
          total: this.data.length
        }
      }
    )
  }
  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort;
    this.loadData();
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
