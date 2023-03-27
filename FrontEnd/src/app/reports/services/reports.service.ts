import {Injectable} from "@angular/core";
import {ApiService} from "../../shared/services/api.service";
import {HttpClient} from "@angular/common/http";
import {AuthenticationService} from "../../shared/services/authentication.service";
import {Router} from "@angular/router";
import {Observable} from "rxjs";
import {ReportInformation} from "../classes/report-information";
import {CaloriesPerUser} from "../classes/calories-per-user";


@Injectable()
export class ReportsService extends ApiService {

  constructor(http: HttpClient, authService: AuthenticationService, router: Router) {
    super(http, authService, router);
  }


  public getReportInformation(): Observable<ReportInformation> {
    return this.get<ReportInformation>('reports/getReportInformation');
  }

  public getAverageCaloriesPerUser(): Observable<CaloriesPerUser[]> {
    return this.get<CaloriesPerUser[]>('reports/getAverageCaloriesPerUser');
  }
}
