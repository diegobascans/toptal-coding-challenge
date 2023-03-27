import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RoutesEnum} from "./common/enums/routes.enum";
import {NoPermissionPageComponent} from "./shared/components/no-permission-page/no-permission-page.component";
import {AdminGuardService} from "./shared/services/admin-guard.service";

const routes: Routes = [
  { path: '', redirectTo: RoutesEnum.Overview, pathMatch: 'full' },

  {
    path: RoutesEnum.Overview,
    loadChildren: () => import('./overview/overview.module').then(m => m.OverviewModule)
  },

  {
    path: RoutesEnum.Admin,
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canLoad: [AdminGuardService]
  },

  {
    path: RoutesEnum.Reports,
    loadChildren: () => import('./reports/reports.module').then(m => m.ReportsModule),
    canLoad: [AdminGuardService]
  },

  { path: RoutesEnum.NoPermission, component: NoPermissionPageComponent },
  { path: '**', pathMatch: 'full', redirectTo: RoutesEnum.Overview }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
