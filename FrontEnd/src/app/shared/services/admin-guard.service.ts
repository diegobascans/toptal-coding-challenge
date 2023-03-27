import {Injectable} from "@angular/core";
import {CanLoad, Route, Router} from "@angular/router";
import {AuthenticationService} from "./authentication.service";
import {RoutesEnum} from "../../common/enums/routes.enum";
import {map, Observable, skipWhile, take, tap} from "rxjs";
import {RolesEnum} from "../../common/enums/roles.enum";

@Injectable()
export class AdminGuardService implements CanLoad {
  constructor(private router: Router, private authService: AuthenticationService) {
  }

  canLoad(route: Route): Observable<boolean> {
    return this.authService.authenticatedRoleSubject
      .pipe(
        skipWhile(user => !user),
        tap(user => {
          if (user.role != RolesEnum.Admin) {
            this.router.navigate([RoutesEnum.Overview]);
          }
        }),
        take(1),
        map(() => true)
      );
  }
}
