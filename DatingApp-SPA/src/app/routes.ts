import { Route, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guardes/auth.guard';
import { ContentChildren } from '@angular/core';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberListResolver } from './_resolver/member-list.resolver';
<<<<<<< HEAD
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
=======
>>>>>>> master

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
<<<<<<< HEAD
        path: 'members',
=======
        path: 'member',
>>>>>>> master
        component: MemberListComponent,
        resolve: { user: MemberListResolver }
      },
      {
<<<<<<< HEAD
        path: 'members/:id',
        component: MemberDetailComponent,
        resolve: { user: MemberDetailResolver }
      },
      {
        path: 'member/edit',
        component: MemberEditComponent,
        resolve: {user: MemberEditResolver}
      },
=======
        path: 'member/:id',
        component: MemberDetailComponent,
        resolve: { user: MemberDetailResolver }
      },
>>>>>>> master
      { path: 'messages', component: MessagesComponent },
      { path: 'lists', component: ListsComponent }
    ]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
