import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SettingsRoutingModule } from './settings-routing.module';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';
import { EmailSettingsComponent } from './email-settings/email-settings.component';
import { MediaSettingsComponent } from './media-settings/media-settings.component';
import { ContentSettingsComponent } from './content-settings/content-settings.component';
import { DiscussionSettingsComponent } from './discussion-settings/discussion-settings.component';


@NgModule({
  declarations: [
    GeneralSettingsComponent,
    EmailSettingsComponent,
    MediaSettingsComponent,
    ContentSettingsComponent,
    DiscussionSettingsComponent
  ],
  imports: [
    CommonModule,
    SettingsRoutingModule
  ]
})
export class SettingsModule { }
