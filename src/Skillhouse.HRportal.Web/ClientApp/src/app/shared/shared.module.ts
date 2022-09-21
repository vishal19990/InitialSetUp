import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AsideComponent } from './components/aside/aside.component';
import { BreadcrumbsComponent } from './components/breadcrumbs/breadcrumbs.component'
import { HeaderComponent } from './components/header/header.component';
import { NavComponent } from './components/nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from '../core/interceptors/jwt.interceptor';
import { PrimeNGModule } from './prime-ng/prime-ng.module';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { LoaderComponent } from './components/loader/loader.component';
import { LoaderService } from './components/loader/loader.service';
import { ToastComponent } from './components/toast/toast.component';
import { NgxEditorModule } from "ngx-editor";


var modules: any[] = [
  CommonModule,
  FormsModule,
  RouterModule,
  ReactiveFormsModule,
  CoreModule,
  PrimeNGModule, 
  NgxEditorModule
  
];

var declarations: any[] = [
  HeaderComponent,
  NavComponent,
  AsideComponent,
  BreadcrumbsComponent,
  ConfirmDialogComponent,
  LoaderComponent,
  ToastComponent,
];

@NgModule({
  declarations: [declarations],
  imports: [modules],
  exports: [modules, declarations],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    LoaderService,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SharedModule {}
