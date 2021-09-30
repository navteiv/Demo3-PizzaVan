import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExcelExportData } from '@progress/kendo-angular-excel-export';
import { GridDataResult, PageChangeEvent, slice } from '@progress/kendo-angular-grid';
import { orderBy, SortDescriptor } from '@progress/kendo-data-query';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DataService } from 'src/app/core/services/data.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @ViewChild('addModal') public addModal!: ModalDirective
  @ViewChild('editModal') public editModal!: ModalDirective
  @ViewChild('deleteModal') public deleteModal!: ModalDirective
  userForm!: FormGroup;
  currentId!: number;

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
      password: [''],
      confirmPassword: ['']
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
    this.userForm.reset();
    this.addModal.show();
  }
  submitForm(){
    console.log(this.userForm.value);
    console.log(JSON.stringify(this.userForm.value));
    this.dataService.post('api/User',JSON.stringify(this.userForm.value))
    .subscribe((response: any) => {
      console.log(response);
      this.loadData();
      this.addModal.hide();
      alert("Thêm thành công");
    }, err => this.dataService.handleError(err));
  }
  showEditModal(id: any): void {
    this.loadUser(id);
    this.editModal.show();
  }
  loadUser(id: any){
    this.dataService.get('api/User/' + id + "?id=" + id).subscribe((response: any) => {
      this.userForm.controls['userName'].setValue(response.userName);
      this.userForm.controls['fullName'].setValue(response.fullName);
      this.userForm.controls['email'].setValue(response.email);
      this.userForm.controls['title'].setValue(response.title);
      this.userForm.controls['dob'].setValue(response.dob.slice(0,10));
    });
    this.currentId = id;
  }
  submitEditForm(){
    this.dataService.put('api/User/' + this.currentId,JSON.stringify(this.userForm.value))
    .subscribe((response: any) => {
      console.log(response);
      this.loadData();
      this.editModal.hide();
      alert("Cập nhật thành công")
    }, err => this.dataService.handleError(err));
  }
  cancel(): void{
    this.addModal.hide();
    this.editModal.hide();
    this.deleteModal.hide();
    this.userForm.reset();
  }

  showDeleteDialog(id: any): void{
    this.deleteModal.show();
    this.currentId = id;
  }
  Delete(){
    this.dataService.delete('api/User/'+this.currentId)
    .subscribe((response: any) => {
      this.loadData();
      this.deleteModal.hide();
    }, err => this.dataService.handleError(err));
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
