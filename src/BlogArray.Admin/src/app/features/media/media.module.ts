import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MediaRoutingModule } from './media-routing.module';
import { MediaLibraryComponent } from './media-library/media-library.component';
import { MediaUploadComponent } from './media-upload/media-upload.component';


@NgModule({
  declarations: [
    MediaLibraryComponent,
    MediaUploadComponent
  ],
  imports: [
    CommonModule,
    MediaRoutingModule
  ]
})
export class MediaModule { }
