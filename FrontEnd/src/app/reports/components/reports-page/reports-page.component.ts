import {Component, OnInit} from '@angular/core';
import {ReportsService} from "../../services/reports.service";
import {finalize, forkJoin, Observable, tap} from "rxjs";
import {ReportInformation} from "../../classes/report-information";
import {DATE_FORMAT} from "../../../common/constans/general.constants";
import {CaloriesPerUser} from "../../classes/calories-per-user";

@Component({
  selector: 'app-reports-page',
  templateUrl: './reports-page.component.html',
  styleUrls: ['./reports-page.component.scss']
})
export class ReportsPageComponent implements OnInit {
  readonly dateFormat = DATE_FORMAT;
  dataSource: CaloriesPerUser[] = [];
  reportInformation: ReportInformation;
  displayedColumns: string[] = ['userId', 'username', 'calories'];

  loading: boolean;

  constructor(private reportsService: ReportsService) {
  }

  ngOnInit(): void {
    const subscriptions: Observable<any>[] = [];

    this.loading = true;
    subscriptions.push(this.reportsService.getReportInformation().pipe(tap((response: ReportInformation) => {
      this.reportInformation = response
    })));

    subscriptions.push(this.reportsService.getAverageCaloriesPerUser().pipe(tap((response: CaloriesPerUser[]) => {
      this.dataSource = response
    })));

    forkJoin(subscriptions).subscribe(() => {
      this.loading = false;
    });
  }

}
