import { List, ListItem, Typography } from '@mui/material'
import axios from 'axios'
import { Fragment, useEffect, useState } from 'react'


function App() {
  const [activties, setActivities] = useState<Activity[]>([])
  useEffect(() => {
    axios.get<Activity[]>('http://localhost:5000/api/Activities').then(response => setActivities(response.data))
    return () => { }
    // const tru: Activity[] = [{
    //   id: "05e4e767-1d25-43b1-8c1c-53f99a8e603e",
    //   title: "Test Create Activity updated 0",
    //   date: "2024-09-26T00:00:00",
    //   description: "Description of the test event updated",
    //   category: "Culture",
    //   city: "London",
    //   venue: "Tower of London updated"
    // }, {
    //   id: "05e4e767-1d25-43b1-8c1c-53f99a8e603e",
    //   title: "Test Create Activity updated 0",
    //   date: "2024-09-26T00:00:00",
    //   description: "Description of the test event updated",
    //   category: "Culture",
    //   city: "London",
    //   venue: "Tower of London updated"
    // },
    // {
    //   id: "05e4e767-1d25-43b1-8c1c-53f99a8e603e",
    //   title: "Test Create Activity updated 0",
    //   date: "2024-09-26T00:00:00",
    //   description: "Description of the test event updated",
    //   category: "Culture",
    //   city: "London",
    //   venue: "Tower of London updated"
    // },
    // {
    //   id: "05e4e767-1d25-43b1-8c1c-53f99a8e603e",
    //   title: "Test Create Activity updated 0",
    //   date: "2024-09-26T00:00:00",
    //   description: "Description of the test event updated",
    //   category: "Culture",
    //   city: "London",
    //   venue: "Tower of London updated"
    // }]
    // setActivities(tru)
  }, [])
  return (
    <Fragment>
      <Typography variant='h3'>Reactivities</Typography>
      <List>
        {activties.map((activity) => (
          <ListItem key={activity.id}>{activity.title}</ListItem>
        ))}
      </List>
    </Fragment>


  )
}

export default App
