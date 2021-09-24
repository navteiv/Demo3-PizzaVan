import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExcelExportData } from '@progress/kendo-angular-excel-export';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { orderBy, SortDescriptor } from '@progress/kendo-data-query';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DataService } from 'src/app/core/services/data.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @ViewChild('addEditModal') public addEditModal!: ModalDirective
  userForm!: FormGroup;

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
      field: "userId",
      dir: "asc",
    },
  ];
  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort;
    this.loadData();
  }

  constructor(private dataService: DataService, private fb: FormBuilder) {
    this.userForm = this.fb.group({
      userName: ['', Validators.required],
      fullName: ['', Validators.required],
      email: ['', Validators.required],
      title: ['', Validators.required],
      dob: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }
  public gridData!: any[];
  ngOnInit(): void {

    this.loadData();
    this.allData = this.allData.bind(this);
  }
  loadData(){
    this.dataService.get('api/User').subscribe(
      (response: any) => {
        this.data = response;
        this.gridView = {
          data: orderBy(this.data, this.sort).slice(this.skip, this.skip + this.pageSize),
          total: this.data.length
        }
      }
    )
  }
  showModal(): void {
    this.addEditModal.show();
  }
  submitForm(): void{
    console.log(this.userForm.value);
    console.log(JSON.stringify(this.userForm.value));
    this.dataService.post('api/User',JSON.stringify(this.userForm.value))
    .subscribe((response: any) => {
      console.log(response);
      this.loadData();
      this.addEditModal.hide();
    }, err => this.dataService.handleError(err));
  }
  cancel(): void{
    this.addEditModal.hide();
    this.userForm.reset();
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
