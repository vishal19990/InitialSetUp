import { Component } from '@angular/core';
import { AuthModel } from './auth/models/login.model';
import { AuthenticationService } from './core/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Student Management System';
  currentUser: AuthModel;

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.currentUser.subscribe(
      (x) => (
        this.currentUser = x
      )
    );
  }
}
