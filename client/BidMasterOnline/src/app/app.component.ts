import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private readonly authService: AuthService,
    private readonly router: Router) {
    this.authService = authService;
  }

  get user() {
    return this.authService.user;
  }

  onLogOutClick() {
    this.authService.logOut();
  }
}