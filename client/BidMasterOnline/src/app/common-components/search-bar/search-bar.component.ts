import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DeepLinkingService } from 'src/app/services/deep-linking.service';

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
  onSubmit = new EventEmitter<void>();

  constructor(private readonly deepLinkingService: DeepLinkingService) {
  }

  async onSearchPressed() {
    await this.deepLinkingService.setQueryParam('searchTerm', this.searchTerm);
    this.onSubmit.emit();
  }
}
