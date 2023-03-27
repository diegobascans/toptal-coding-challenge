import {NgModule} from '@angular/core';
import {SharedModule} from "../shared/shared.module";
import {AdminPageComponent} from "./components/admin-page/admin-page.component";
import {AdminRoutingModule} from "./admin-routing.module";
import {AdminService} from "./services/admin.service";
import {MealFormComponent} from "./components/meal-form/meal-form.component";

@NgModule({
  declarations: [AdminPageComponent, MealFormComponent],
  imports: [SharedModule, AdminRoutingModule],
  providers: [AdminService],
})
export class AdminModule {
}
