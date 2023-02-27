import { useEffect, useState } from 'react';
import { apiPost } from '../../service/Api'
import {APIConst} from './../../service/apiEndPoints';
import {useNavigate} from 'react-router-dom';
// material-ui
import {
    Button,
    FormHelperText,
    Grid,
    InputLabel,
    OutlinedInput,
    Stack,
    Box
} from '@mui/material';

// third party
import * as Yup from 'yup';
import { Formik } from 'formik';

// project import
import AnimateButton from 'components/@extended/AnimateButton';
import { strengthColor, strengthIndicator } from 'utils/password-strength';

// assets 

// ============================|| FIREBASE - REGISTER ||============================ //

const ConstructionForm = () => {
    const [level,setLevel] = useState();
    const [showPassword, setShowPassword] = useState(false);
    const navigate  =  useNavigate();
    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };

    const changePassword = (value) => {
        const temp = strengthIndicator(value);
        setLevel(strengthColor(temp));
    };

    useEffect(() => {
        changePassword('');
    }, []);

    return (
        <>
            <Formik
                initialValues={{
                    ownerName:'',
                    address: '',
                    price:1000,
                    submit: null
                }}
                validationSchema={Yup.object().shape({
                    ownerName: Yup.string().max(255).required('Owner name is required'),
                    address: Yup.string().max(255).required('Address is required'),
                    price: Yup.number().required('Construction price  is required'),
                    
                })}
                onSubmit={async (values, { setErrors, setStatus, setSubmitting }) => {
                    try {
                       
                        apiPost(APIConst.Constructions,values)
                        .then(res=>{
                            setStatus({ success: false });
                            localStorage.setItem('ConstructionId',res.data.id);
                            localStorage.setItem('Amount',res.data.price);
                            navigate('/onlinePayment');
                        })
                        setSubmitting(false);
                    } catch (err) {
                        console.error(err);
                        setStatus({ success: false });
                        setErrors({ submit: err.message });
                        setSubmitting(false);
                    }
                }}
            >
                {({ errors, handleBlur, handleChange, handleSubmit, isSubmitting, touched, values }) => (
                   <Box>
                 <form noValidate onSubmit={handleSubmit}>
                      
                        <Grid container spacing={3}>
                            <Grid item md={6}>
                                <Stack spacing={1}>
                                    <InputLabel htmlFor="firstname-signup">Owner Name*</InputLabel>
                                    <OutlinedInput
                                        id="ownerName-login"
                                        value={values.ownerName}
                                        name="ownerName"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        placeholder="Owner Name"
                                        fullWidth
                                        error={Boolean(touched.ownerName && errors.ownerName)}
                                    />
                                    {touched.ownerName && errors.ownerName && (
                                        <FormHelperText error id="helper-text-firstname-signup">
                                            {errors.ownerName}
                                        </FormHelperText>
                                    )}
                                </Stack>
                            </Grid>
                            <Grid item xs={6}>
                                </Grid>
                            <Grid item xs={6}>
                                <Stack spacing={1}>
                                    <InputLabel htmlFor="lastname-signup">Address *</InputLabel>
                                    <OutlinedInput
                                        fullWidth
                                        error={Boolean(touched.lastname && errors.lastname)}
                                        id="address"
                                        type="address"
                                        value={values.address}
                                        name="address"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        placeholder="address"
                                        inputProps={{}}
                                    />
                                    {touched.address && errors.address && (
                                        <FormHelperText error id="helper-text-lastname-signup">
                                            {errors.address}
                                        </FormHelperText>
                                    )}
                                </Stack>
                            </Grid>
                            <Grid item xs={6}>
                                </Grid>
                            <Grid item xs={6}>
                                <Stack spacing={1}>
                                    <InputLabel htmlFor="company-signup">Construction price</InputLabel>
                                    <OutlinedInput
                                        fullWidth
                                        type="number"
                                        error={Boolean(touched.price && errors.price)}
                                        id="company-signup"
                                        value={values.price}
                                        name="price"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        placeholder="price"
                                        inputProps={{}}
                                    />
                                    {touched.price && errors.price && (
                                        <FormHelperText error id="helper-text-company-signup">
                                            {errors.price}
                                        </FormHelperText>
                                    )}
                                </Stack>
                            </Grid>
                            {errors.submit && (
                                <Grid item xs={12}>
                                    <FormHelperText error>{errors.submit}</FormHelperText>
                                </Grid>
                            )}
                                <Grid item xs={6}>
                                </Grid>
                            <Grid item xs={1}>
                                <AnimateButton>
                                    <Button
                                        disableElevation
                                        disabled={isSubmitting}
                                        fullWidth
                                        size="large"
                                        type="submit"
                                        variant="contained"
                                        color="primary"
                                    >
                                        Submit
                                    </Button>
                                </AnimateButton>
                            </Grid>
                        </Grid>
                    </form>
                    </Box>
                )}
            </Formik>
        </>
    );
};

export default ConstructionForm;
