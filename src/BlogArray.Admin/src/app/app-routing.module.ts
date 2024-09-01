import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PortalLayoutComponent } from './layout/portal-layout/portal-layout.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        loadChildren: () => import('./features/dashboard/dashboard.module').then(m => m.DashboardModule),
      },
      {
        path: 'posts',
        loadChildren: () => import('./features/posts/posts.module').then(m => m.PostsModule),
      },
    ],
    component: PortalLayoutComponent
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
