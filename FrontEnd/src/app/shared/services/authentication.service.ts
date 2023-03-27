import {Injectable} from "@angular/core";
import {BehaviorSubject} from "rxjs";
import {User} from "../../common/classes/user";

@Injectable({providedIn: 'root',})
export class AuthenticationService {

  // @ts-ignore
  public authenticatedRoleSubject: BehaviorSubject<User> = new BehaviorSubject<User>(null);

  authenticated: boolean = false;
  token: string;
  private _user: User;

  set user(user: User) {
    this._user = user;
    this.authenticatedRoleSubject.next(this._user);
  }

  get userCaloriesLimit(): number {
    return this._user.caloriesLimit;
  }
}
