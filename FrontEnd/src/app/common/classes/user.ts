import {RolesEnum} from "../enums/roles.enum";

export class User {
  id: number;
  username: string
  role: RolesEnum
  caloriesLimit: number;
}
