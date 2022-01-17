import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss'],
})
export class PagerComponent implements OnInit {
  @Input() pageSize = 0;
  @Input() totalCount = 0;

  @Output() pageChanged = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}

  onPageChanged(event: any): void {
    this.pageChanged.emit(event);
  }
}
