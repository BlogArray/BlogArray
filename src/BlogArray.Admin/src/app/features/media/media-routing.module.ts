import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MediaLibraryComponent } from './media-library/media-library.component';
import { MediaUploadComponent } from './media-upload/media-upload.component';

const routes: Routes = [
  { path: '', redirectTo: 'library', pathMatch: 'full' },
  { path: 'library', component: MediaLibraryComponent },
  { path: 'upload', component: MediaUploadComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MediaRoutingModule { }
