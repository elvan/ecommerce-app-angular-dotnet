import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  declarations: [],
  imports: [CommonModule, PaginationModule.forRoot()],
  exports: [PaginationModule],
})
export class SharedModule {}
