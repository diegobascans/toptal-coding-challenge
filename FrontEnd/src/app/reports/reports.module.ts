import {NgModule} from '@angular/core';
import {SharedModule} from "../shared/shared.module";
import {ReportsRoutingModule} from "./reports-routing.module";
import {ReportsPageComponent} from "./components/reports-page/reports-page.component";
import {ReportsService} from "./services/reports.service";

@NgModule({
  declarations: [ReportsPageComponent],
  imports: [SharedModule, ReportsRoutingModule],
  providers: [ReportsService],
})
export class ReportsModule {
}
