import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from "./shared/services/authentication.service";
import {Router} from "@angular/router";
import {RoutesEnum} from "./common/enums/routes.enum";
import {GeneralService} from "./shared/services/general.service";
import {USER_ID} from "./common/constans/general.constants";
import {finalize} from "rxjs";
import {RolesEnum} from "./common/enums/roles.enum";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public title = 'Calories Tracking App';
  public icon = 'fitness_center';
  public isAuthenticated: boolean = false;
  public isAdmin: boolean = false;
  public username: string;
  public isLoading: boolean = true;

  constructor(private authService: AuthenticationService, private generalService: GeneralService, private router: Router) {
  }

  ngOnInit(): void {
    this.generalService.authenticate(USER_ID).pipe(finalize(() => {
      this.isLoading = false;
    })).subscribe({ next: (response) => {
      if (response && response.token) {
        this.authService.token = response.token;
        this.authService.authenticated = true;

        if (this.authService.authenticated) {
          this.isAuthenticated = true;
          this.isAdmin = response.user.role == RolesEnum.Admin;
          this.username = response.user.username;
          this.authService.user = response.user;
        } else {
          this.router.navigate([RoutesEnum.NoPermission]);
        }
      } else {
        this.router.navigate([RoutesEnum.NoPermission]);
      }
    }, error: () => {
      this.router.navigate([RoutesEnum.NoPermission]);
    }});
  }
}
