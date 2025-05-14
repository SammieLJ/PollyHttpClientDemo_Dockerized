const express = require('express');
const app = express();
const port = 3000;

app.get('/unstable', (req, res) => {
  if (Math.random() < 0.5) {
    res.status(500).send("Random failure occurred!");
  } else {
    res.send("Success from mock API!");
  }
});

app.listen(port, () => {
  console.log(`Mock API running on port ${port}`);
});
