import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ButtonModule } from '../../shared/ui/button/button.module';
import { IconsModule } from "../../shared/ui/icons/icons.module";
import { InputModule } from '../../shared/ui/input/input.module';


@NgModule({
  declarations: [
    LoginComponent,
    LogoutComponent,
    ForgotPasswordComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ButtonModule,
    InputModule,
    IconsModule
  ]
})
export class AuthModule { }
