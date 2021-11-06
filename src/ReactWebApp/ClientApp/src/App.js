import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import { LeagueTable } from './components/Matches/LeagueTable';
import Seasons from './components/Seasons/Seasons';
import Matches from './components/Matches/Matches';
import MatchStats from './components/Matches/MatchStats';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/league-table' component={LeagueTable} />
        <Route path='/seasons' component={Seasons} />
        <Route path='/matches' component={Matches} />
        <Route path='/match-stats/:matchId' component={MatchStats} />
      </Layout>
    );
  }
}
