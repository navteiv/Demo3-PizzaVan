import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index.component';
import { NgxPaginationModule } from 'ngx-pagination';

const indexRoutes: Routes = [
  {path: '', component: IndexComponent}
]

@NgModule({
  declarations: [IndexComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(indexRoutes),
    NgxPaginationModule
  ]
})
export class IndexModule { }
