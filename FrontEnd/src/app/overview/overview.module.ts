import {NgModule} from '@angular/core';
import {SharedModule} from "../shared/shared.module";
import {OverviewPageComponent} from "./components/overview-page/overview-page.component";
import {OverviewRoutingModule} from "./overview-routing.module";
import {NewMealComponent} from "./components/new-meal/new-meal.component";
import {OverviewService} from "./services/overview.service";
import {UserService} from "./services/user.service";
import {CheatDietComponent} from "./components/cheat-diet/cheat-diet.component";

@NgModule({
  declarations: [OverviewPageComponent, NewMealComponent, CheatDietComponent],
  imports: [SharedModule, OverviewRoutingModule],
  providers: [OverviewService, UserService],
})
export class OverviewModule {
}
