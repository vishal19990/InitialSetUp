import { Component, OnInit } from "@angular/core";
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { first } from "rxjs/operators";
import { AuthenticationService } from "../core/services/authentication.service";
import { AuthModel } from "./models/login.model";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  returnUrl: string;
  loggedIn: boolean;
  submitted: Boolean = false;
  errorMessage: string;
  error = "";

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder
  ) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(["/"]);
    }
  }

  ngOnInit(): void {
    this.returnUrl = "/";

    this.loginForm = this.formBuilder.group({
      username: ["", Validators.required],
      password: ["", Validators.required],
    });
  }

  // convenience getter for easy access to form fields
  get f(): { [key: string]: AbstractControl } {
    return this.loginForm.controls;
  }

  login() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    const userName = this.loginForm.controls.username.value;
    const password = this.loginForm.controls.password.value;
    debugger
    var data=new AuthModel();
    data ={
      
        userId: 1,
        userName: "test",
        tokenExpiryDate: "string",
        token: "string",
        error: null
      
    }
    this.authenticationService.setUserContext(data)
    this.router.navigateByUrl(this.returnUrl);


    // this.authenticationService
    //   .login(userName, password)
    //   .pipe(first())
    //   .subscribe(
    //     (data) => {
    //       if (data.error == null) {
    //         this.loggedIn = true;
    //         this.authenticationService.setUserContext(data);
    //         this.router.navigateByUrl(this.returnUrl);
    //       }
    //       this.errorMessage = data.error;
    //       this.submitted = false;
    //       this.loginForm.reset();

    //       if (this.errorMessage != null) {
    //         this.error = this.errorMessage;
    //       }
    //     },
    //     (error) => {
    //       this.error = error;
    //       this.submitted = false;
    //       this.loginForm.reset();
    //     }
    //   );
  }
}
