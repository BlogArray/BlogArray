import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormDirective } from './form.directive';
import { FormLabelDirective } from './form-label.directive';


@NgModule({
  declarations: [
    FormDirective,
    FormLabelDirective,
  ],
  imports: [
    CommonModule
  ], exports: [
    FormDirective,
    FormLabelDirective,
  ]
})
export class TwFormModule { }
