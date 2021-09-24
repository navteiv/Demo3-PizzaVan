import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { RouterModule, Routes } from '@angular/router';
import { ExcelModule, GridModule, PDFModule } from '@progress/kendo-angular-grid';
import { ModalModule} from 'ngx-bootstrap/modal';
import { ReactiveFormsModule } from '@angular/forms';
const userRoutes: Routes = [
  {path:'', redirectTo:'index', pathMatch:'full'},
  {path:'index', component: UserComponent}
]


@NgModule({
  declarations: [
    UserComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(userRoutes),
    GridModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    PDFModule,
    ExcelModule
  ]
})
export class UserModule { }
