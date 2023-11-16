import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  placeholder: string = 'Search auction...';

  onSearchPressed(searchTerm: string) {
    alert(searchTerm);
  }
}
