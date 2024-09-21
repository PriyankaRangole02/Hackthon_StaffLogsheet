import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Header from '../MYComponents/Header';
import Footer from '../MYComponents/Footer';
import { useNavigate } from "react-router-dom";
import {
    Container,Paper, Typography, TextField, Select, MenuItem, Button, Card, CardContent, Grid, InputLabel, FormControl, CircularProgress
} from '@mui/material';
import { DateTimePicker} from '@mui/x-date-pickers';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';



const AddLogs = () => {
    const [courses, setCourses] = useState([]);
    const [subjects, setSubjects] = useState([]);
    const [loading, setLoading] = useState(false);
    const [logSheetData, setLogSheetData] = useState({
        startTime: '',
        endTime: '',
        logType: '',
        courseId: '',
        subjectId: '',
        topic: '',
    });
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    const token = sessionStorage.getItem('token');
    const userId = sessionStorage.getItem('id'); // Assuming userId is stored in localStorage
    const [username, setUsername] = useState('');
    console.log(userId);

    const navigate = useNavigate()

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await axios.get('http://localhost:5063/course', {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setCourses(response.data);
            } catch (err) {
                setError('Failed to fetch courses.');
            }
            const storedUsername = sessionStorage.getItem('name');
            if (storedUsername) {
                setUsername(storedUsername);
            }
        };

        const fetchSubjects = async () => {
            try {
                const response = await axios.get('http://localhost:5063/api/Subject', {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setSubjects(response.data);
            } catch (err) {
                setError('Failed to fetch subjects.');
            }
        };

        fetchCourses();
        fetchSubjects();
    }, [token]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setLogSheetData({
            ...logSheetData,
            [name]: value,
        });
    };
        const handleDateChange = (name, newValue) => {
            setLogSheetData({
            ...logSheetData,
            [name]: newValue,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');
        setLoading(true);

        if (!logSheetData.startTime || !logSheetData.endTime || !logSheetData.logType || !logSheetData.courseId || !logSheetData.subjectId || !logSheetData.topic) {
            setError('Please fill out all fields.');
            setLoading(false);
            return;
        }

        const logSheetToSubmit = { ...logSheetData, userId: parseInt(userId, 10) };
        console.log(logSheetToSubmit);

        try {
            const response = await axios.post('http://localhost:5063/api/LogSheet/create', logSheetToSubmit, {
                headers: { Authorization: `Bearer ${token}` },
            });
            if (response.status === 201) {
                setSuccess('Log sheet created successfully!');
                setLogSheetData({
                    startTime: '',
                    endTime: '',
                    logType: '',
                    courseId: '',
                    subjectId: '',
                    topic: '',
                });
                navigate('/loglist')
            }
        } catch (err) {
            setError('Failed to create log sheet.');
        }
        setLoading(false);
    };

    return (
    <div>
        <Header username={username} />  
        <Container maxWidth="md" sx={{ mt: 4 }}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
            {/* <Card>
                <CardContent> */}
                    <Typography variant="h4" component="h1" gutterBottom align="center">
                        Create Log Sheet
                    </Typography>

                    {error && <Typography color="error" align="center">{error}</Typography>}
                    {success && <Typography color="success" align="center">{success}</Typography>}

                    <form onSubmit={handleSubmit}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6} md={6} >
                        <Paper elevation={3} sx={{ padding: 2 }}>
                            <DateTimePicker
                                label="Start Time"
                                value={logSheetData.startTime}
                                onChange={(newValue) => handleDateChange('startTime', newValue)}
                                renderInput={(props) => <TextField {...props} fullWidth required />}
                                sx={{width:"100%"}}
                            />
                        </Paper>    
                        </Grid>
                        <Grid item xs={12}  sm={6} md={6}>
                        <Paper elevation={3} sx={{ padding: 2 }}>
                            <DateTimePicker
                                label="End Time"
                                value={logSheetData.endTime}
                                onChange={(newValue) => handleDateChange('endTime', newValue)}
                                renderInput={(props) => <TextField {...props} fullWidth required />}
                                sx={{width:"100%"}}
                            />
                        </Paper>    
                        </Grid>

                            {/* <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Start Time"
                                    name="startTime"
                                    type="datetime-local"
                                    value={logSheetData.startTime}
                                    onChange={handleInputChange}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    required
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="End Time"
                                    name="endTime"
                                    type="datetime-local"
                                    value={logSheetData.endTime}
                                    onChange={handleInputChange}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    required
                                />
                            </Grid> */}
                            <Grid item xs={12} sm={6} md={12}>
                            <Paper elevation={3} sx={{ padding: 2 }}>

                            <FormControl fullWidth required>
                                <InputLabel>Log Type</InputLabel>
                                <Select name="logType" value={logSheetData.logType} onChange={handleInputChange}>
                                    <MenuItem value="Theory">Theory</MenuItem>
                                    <MenuItem value="Lab">Lab</MenuItem>
                                    <MenuItem value="TU">Technical Upgradation</MenuItem>
                                    <MenuItem value="SC">Student Convenience</MenuItem>
                                    <MenuItem value="CM">Course Material</MenuItem>
                                    <MenuItem value="ES">Event Support</MenuItem>
                                </Select>
                            </FormControl>
                                {/* <TextField
                                    fullWidth
                                    label="Log Type"
                                    name="logType"
                                    value={logSheetData.logType}
                                    onChange={handleInputChange}
                                    required
                                /> */}
                            </Paper>    
                            </Grid>
                            <Grid item xs={12} sm={6} md={12}>
                            <Paper elevation={3} sx={{ padding: 2 }}>
    
                                <FormControl fullWidth required>
                                    <InputLabel id="course-label">Course</InputLabel>
                                    <Select
                                        labelId="course-label"
                                        name="courseId"
                                        value={logSheetData.courseId}
                                        onChange={handleInputChange}
                                    >
                                        <MenuItem value=""><em>Select Course</em></MenuItem>
                                        {courses.map((course) => (
                                            <MenuItem key={course.courseId} value={course.courseId}>
                                                {course.courseName}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Paper>    
                            </Grid>
                            <Grid item xs={12} sm={6} md={12}>
                            <Paper elevation={3} sx={{ padding: 2 }}>
                                <FormControl fullWidth required>
                                    <InputLabel id="subject-label">Subject</InputLabel>
                                    <Select
                                        labelId="subject-label"
                                        name="subjectId"
                                        value={logSheetData.subjectId}
                                        onChange={handleInputChange}
                                    >
                                        <MenuItem value=""><em>Select Subject</em></MenuItem>
                                        {subjects.map((subject) => (
                                            <MenuItem key={subject.subjectId} value={subject.subjectId}>
                                                {subject.subjectName}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Paper>
                            </Grid>
                            <Grid item xs={12} sm={6} md={12}>
                            <Paper elevation={3} sx={{ padding: 2 }}>
                                <TextField
                                    fullWidth
                                    label="Topic"
                                    name="topic"
                                    value={logSheetData.topic}
                                    onChange={handleInputChange}
                                    required
                                />
                            </Paper>
                            </Grid>
                            <Grid item xs={12} sm={6} md={12}>
                            <Paper elevation={3} sx={{ padding: 2 }}>
                                <Button
                                    type="submit"
                                    fullWidth
                                    variant="contained"
                                    color="primary"
                                    disabled={loading}
                                >
                                    {loading ? <CircularProgress size={24} /> : 'Add Log'}
                                </Button>
                            </Paper>
                            </Grid>
                        </Grid>
                    </form>
                {/* </CardContent>
            </Card> */}
          </LocalizationProvider >
        </Container>
        <Footer sx={{ mt: 10, mb: 4 }}/>
    </div>  
    );
};

export default AddLogs;
