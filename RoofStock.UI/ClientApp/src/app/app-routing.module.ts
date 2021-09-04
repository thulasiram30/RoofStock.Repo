import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavigationModule } from './navigation/navigation.module';
import { NavigationComponent } from './navigation/navigation/navigation.component';

const routes: Routes = [
  // {
  // path: '',
  // loadChildren: () =>
  //   import('./login/login.module').then(
  //     m => m.LoginModule
  //   ),
  // },
  {
  path: '',
  component: NavigationComponent,
  children: [
              {
                path: '',
                redirectTo: '/Properties',
                pathMatch: 'full'
              },
              {
                path: 'Properties',
                loadChildren: () =>
                  import('./property/property.module').then(
                    m=>m.PropertyModule
                  )
              },
              {
                path: 'Portfolio',
                loadChildren: () =>
                  import('./portfolio/portfolio.module').then(
                    m=>m.PortfolioModule
                  )
              },
              {
                path: 'Market',
                loadChildren: () =>
                  import('./market/market.module').then(
                    m=>m.MarketModule
                  )
              }
            ]
  }
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    NavigationModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
