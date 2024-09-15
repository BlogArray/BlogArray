import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';
import { EmailSettingsComponent } from './email-settings/email-settings.component';
import { ContentSettingsComponent } from './content-settings/content-settings.component';
import { MediaSettingsComponent } from './media-settings/media-settings.component';
import { DiscussionSettingsComponent } from './discussion-settings/discussion-settings.component';

const routes: Routes = [
  { path: 'general', component: GeneralSettingsComponent },
  { path: 'email', component: EmailSettingsComponent },
  { path: 'content', component: ContentSettingsComponent },
  { path: 'media', component: MediaSettingsComponent },
  { path: 'discussion', component: DiscussionSettingsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
