import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExcelExportData } from '@progress/kendo-angular-excel-export';
import { DataStateChangeEvent, GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { orderBy, SortDescriptor, process, State } from '@progress/kendo-data-query';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { SystemConstants } from 'src/app/core/common/constants';
import { DataService } from 'src/app/core/services/data.service';

@Component({
  selector: 'app-dish',
  templateUrl: './dish.component.html',
  styleUrls: ['./dish.component.css', "../../../assets/css/pdf-export.css"]
})

export class DishComponent implements OnInit {
  public message!: string;
  public progress!: number;
  @Output() public onUploadFinished = new EventEmitter();

  @ViewChild('addModal') public addModal!: ModalDirective
  @ViewChild('editModal') public editModal!: ModalDirective

  dishForm!: FormGroup;
  currentId!: number;

  public pageSize = 5;
  public skip = 0;
  public loading!: boolean;
  public data!: any[];
  public gridView!: GridDataResult;
  public dbPath!: string;
  public imagePath = SystemConstants.LOCAL_API + "Image/Dish/"

  public type = 'numeric';
  public buttonCount = 5;
  public info = true;
  public previousNext = true;
  public position = 'bottom';

  constructor(private dataService: DataService, private fb: FormBuilder, private http: HttpClient) {
    this.dishForm = this.fb.group({
      name: ['', Validators.required],
      price: ['', Validators.required],
      category: ['', Validators.required],
      image: [''],
      imageFile: [''],
      description: [''],
      status: ['']
    });
  }


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
  cancel(): void{
    this.addModal.hide();
    this.editModal.hide();
    this.dishForm.reset();
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
  showModal():void {
    this.dishForm.reset();
    this.addModal.show();
  }
  submitForm(): void{
    this.dishForm.value['image'] = this.dbPath;
    this.dataService.post('api/Dish', JSON.stringify(this.dishForm.value)).subscribe(
      (response: any) => {
        console.log(this.dishForm.value)
        console.log(response);
        this.loadData();
        this.addModal.hide();
        alert("Thêm món thành công");
      }, err => this.dataService.handleError(err));
  }
  public uploadFile = (files: any) => {
    if(files.length === 0) return;
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.dbPath = fileToUpload.name;
    console.log(this.dishForm.value);
    this.http.post(SystemConstants.LOCAL_API + "api/Upload", formData, {reportProgress: true, observe: 'events'})
    .subscribe(event => {
      if(event.type === HttpEventType.UploadProgress){
        this.progress = Math.round(100* event.loaded / event.total!);
      }
      else if(event.type === HttpEventType.Response){
        this.message = "Upload success";
        this.onUploadFinished.emit(event.body);
      }
    })
  }
}
