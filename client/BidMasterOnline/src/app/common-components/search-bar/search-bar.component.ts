import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent {

  searchTerm: string = '';

  @Input()
  placeholder: string = 'Search...';

  @Output()
  onSubmit = new EventEmitter<string>();

  onSearchPressed() {
    this.onSubmit.emit(this.searchTerm);
  }
}
