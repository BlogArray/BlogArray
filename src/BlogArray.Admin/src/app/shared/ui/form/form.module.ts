import { NgModule } from '@angular/core';
import { TwFormDirective } from './form.directive';
import { TwFormLabelDirective } from './form-label.directive';


@NgModule({
  declarations: [
    TwFormDirective,
    TwFormLabelDirective,
  ], exports: [
    TwFormDirective,
    TwFormLabelDirective,
  ]
})
export class TwFormModule { }
