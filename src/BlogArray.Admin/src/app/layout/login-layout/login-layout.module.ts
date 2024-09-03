import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginLayoutComponent } from './login-layout.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    LoginLayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule
  ]
})
export class LoginLayoutModule { }
