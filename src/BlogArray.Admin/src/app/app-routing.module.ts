import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PortalLayoutComponent } from './layout/portal-layout/portal-layout.component';
import { LoginLayoutComponent } from './layout/login-layout/login-layout.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'dashboard',
        loadChildren: () => import('./features/dashboard/dashboard.module').then(m => m.DashboardModule),
      },
      {
        path: 'posts',
        loadChildren: () => import('./features/posts/posts.module').then(m => m.PostsModule),
      },
      {
        path: 'categories',
        loadChildren: () => import('./features/categories/categories.module').then(m => m.CategoriesModule),
      },
      {
        path: 'users',
        loadChildren: () => import('./features/users/users.module').then(m => m.UsersModule),
      },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    ],
    component: PortalLayoutComponent
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule),
    component: LoginLayoutComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
