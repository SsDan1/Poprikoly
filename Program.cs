using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NXOpen;
using NXOpenUI;
using NXOpen.UF;
using NXOpen.Utilities;
using NX10_Open_CS_Library;
using System.Collections;

public class Program
{
    // class members
    private static Session theSession;
    private static UI theUI;
    private static UFSession theUFSession;
    public static Program theProgram;
    public static bool isDisposeCalled;
    public static double D;
    public static double A;

    //------------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------------
    public Program()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theUFSession = UFSession.GetUFSession();
            isDisposeCalled = false;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }

    //------------------------------------------------------------------------------
    //  Explicit Activation
    //      This entry point is used to activate the application explicitly
    //------------------------------------------------------------------------------
    public static int Main(string[] args)
    {
        int retValue = 0;
        try
        {
            theProgram = new Program();

            Form1 NF = new Form1();
            NF.Show();

            


            theProgram.Dispose();
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
        return retValue;
    }

    //------------------------------------------------------------------------------
    // Following method disposes all the class members
    //------------------------------------------------------------------------------

    public static void Vint()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);

        UFCurve.Line Line1 = new UFCurve.Line();
        UFCurve.Line Line2 = new UFCurve.Line();
        UFCurve.Line Line3 = new UFCurve.Line();
        UFCurve.Line Line4 = new UFCurve.Line();
    

        Line1.start_point = new double[3] { 0, 0, 0 };
        Line1.end_point = new double[3] { 0, 0.5, 0 };

        Line2.start_point = Line1.end_point;
        Line2.end_point = new double[3] { 3.2, 0.5, 0 };

        Line3.start_point = Line2.end_point;
        Line3.end_point = new double[3] { 3.2, 0, 0 };

        Line4.start_point = Line3.end_point;
        Line4.end_point = Line1.start_point;

        Tag[] SK1 = new Tag[4];

        theUFSession.Curve.CreateLine(ref Line1, out SK1[0]);
        theUFSession.Curve.CreateLine(ref Line2, out SK1[1]);
        theUFSession.Curve.CreateLine(ref Line3, out SK1[2]);
        theUFSession.Curve.CreateLine(ref Line4, out SK1[3]);

        double[] ref_pt1 = new double[3];
        ref_pt1[0] = 0.00;
        ref_pt1[1] = 0.00;
        ref_pt1[2] = 0.00;
        double[] direction1 = { 1.00, 0.00, 0.00 };
        string[] limit1 = { "0", "360" };
        Tag[] features1;
        theUFSession.Modl.CreateRevolved(SK1, limit1, ref_pt1,
        direction1, FeatureSigns.Nullsign, out features1);
        Tag feat = features1[0];
        Tag[] FeatFaces;
        int FacesCount, FaceType, FaceNormDir;
        Tag face, s_face, c_face, feature_eid;
        double[] point = new double[3];
        double[] dir = new double[3];
        double[] box = new double[6];
        double radius, rad;
        s_face = new Tag();
        c_face = new Tag();
        theUFSession.Modl.AskFeatFaces(feat, out FeatFaces);
        theUFSession.Modl.AskListCount(FeatFaces, out
       FacesCount);
        UFModl.SymbThreadData thread = new
        UFModl.SymbThreadData();
        for (int i = 0; i < FacesCount; i++)
        {
            theUFSession.Modl.AskListItem(FeatFaces, i, out face);
            theUFSession.Modl.AskFaceData(face, out FaceType,
           point, dir, box, out radius, out rad, out FaceNormDir);
            if (FaceType == 22)
            {
                s_face = face;

            }
            if (FaceType == 16)
            {
                c_face = face;
            }
        }
        double[] thread_direction = { 1.00, 0.00, 0.00 };
        // направление резьбы 
        thread.cyl_face = c_face;
        thread.start_face = s_face;
        thread.axis_direction = thread_direction;
        thread.rotation = 1;
        thread.num_starts = 1;
        thread.length = "3.2";// длина резьбы 
        thread.form = "Metric";// метрическая система 
        thread.major_dia = "1";
        //внешний диаметр резьбы 
        thread.minor_dia = "0.8";
        //внутренний диаметр резьбы 
        thread.tapped_dia = "1";//диметр резьбы 
        thread.pitch = "0.25";//шаг резьбы 
        thread.angle = "60";//угол резьбы 
        try
        {
            theUFSession.Modl.CreateSymbThread(ref thread, out
           feat);
        }
        catch (NXOpen.NXException ex)
        {
            UI.GetUI().NXMessageBox.Show("Message",
            NXMessageBox.DialogType.Error, ex.Message);
        }

        UFCurve.Line Line5 = new UFCurve.Line();
        UFCurve.Line Line6 = new UFCurve.Line();
        UFCurve.Line Line7 = new UFCurve.Line();
        UFCurve.Line Line8 = new UFCurve.Line();

        Line5.start_point = new double[3] { 3.2, 0, 0 };
        Line5.end_point = new double[3] { 3.2, 0.5, 0 };

        Line6.start_point = Line5.end_point;
        Line6.end_point = new double[3] { 4.1, 0.1, 0 };

        Line7.start_point = Line6.end_point;
        Line7.end_point = new double[3] { 4.1, 0, 0 };

        Line8.start_point = Line7.end_point;
        Line8.end_point = Line5.start_point;

        Tag[] SK2 = new Tag[4];
        theUFSession.Curve.CreateLine(ref Line5, out SK2[0]);
        theUFSession.Curve.CreateLine(ref Line6, out SK2[1]);
        theUFSession.Curve.CreateLine(ref Line7, out SK2[2]);
        theUFSession.Curve.CreateLine(ref Line8, out SK2[3]);

        double[] ref_pt11 = new double[3];
        ref_pt11[0] = 0.00;
        ref_pt11[1] = 0.00;
        ref_pt11[2] = 0.00;
        double[] direction11 = { 1.00, 0.00, 0.00 };
        string[] limit11 = { "0", "360" };
        Tag[] features11;
        theUFSession.Modl.CreateRevolved(SK2, limit11, ref_pt11,
        direction11, FeatureSigns.Positive, out features11);

        UFCurve.Line Line9 = new UFCurve.Line();
        UFCurve.Line Line10 = new UFCurve.Line();
        UFCurve.Line Line11 = new UFCurve.Line();
        UFCurve.Line Line12 = new UFCurve.Line();
        UFCurve.Line Line13 = new UFCurve.Line();

        Line9.start_point = new double[3] { 0, 0, 0 };
        Line9.end_point = new double[3] { 0, 0.15, 0 };

        Line10.start_point = Line9.end_point;
        Line10.end_point = new double[3] { 0.5, 0.15, 0 };

        Line11.start_point = Line10.end_point;
        Line11.end_point = new double[3] { 0.5, -0.15, 0 };

        Line12.start_point = Line11.end_point;
        Line12.end_point = new double[3] { -0, -0.15, 0 };

        Line13.start_point = Line12.end_point;
        Line13.end_point = Line9.start_point;
        Tag[] SK3 = new Tag[5];
        theUFSession.Curve.CreateLine(ref Line9, out SK3[0]);
        theUFSession.Curve.CreateLine(ref Line10, out SK3[1]);
        theUFSession.Curve.CreateLine(ref Line11, out SK3[2]);
        theUFSession.Curve.CreateLine(ref Line12, out SK3[3]);
        theUFSession.Curve.CreateLine(ref Line13, out SK3[4]);

        string[] Ext1_lim = { "-0.6", "0.6" };
        double[] point1 = new double[3];
        double[] Ext1_dir = { 0, 0, 1 };

        Tag[] Ext1;
        theUFSession.Modl.CreateExtruded(SK3, "0", Ext1_lim, point1, Ext1_dir, FeatureSigns.Negative, out Ext1);

    }
    public static void Postr1()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 3.5;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "0.3" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 1.2;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "0.3" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr2()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 5.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "0.3" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 2.2;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "0.3" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr3()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 7;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "0.5" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 3.2;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "0.5" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }


    public static void Postr4()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 9;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "0.8" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 4.3;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "0.8" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr5()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 10.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "1.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 5.3;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "1.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }
    public static void Postr6()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 12.5;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "1.6" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 6.4;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "1.6" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }


    public static void Postr8()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 17.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "1.6" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 8.4;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "1.6" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr10()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 21.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "2.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 10.5;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "2.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr12()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 24.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "2.5" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 13.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "2.5" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr14()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 28.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "2.5" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 15.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "2.5" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr16()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 30.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "2.5" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 17.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "2.5" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr18()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 34.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "3.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 19.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "3.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr20()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 37.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "3.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 21.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "3.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr22()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 39.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "3.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 23.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "3.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr24()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 44.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "4.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 25.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "4.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr27()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 50.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "4.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 28.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "4.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr30()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 56.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "4.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 31.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "4.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr36()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 66.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "5.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 37.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "5.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr42()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 78.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "7.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 43.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "7.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }

    public static void Postr48()
    {
        Tag UFPart1;
        string name1 = "Model_for_Kurs";
        int units1 = 1;
        theUFSession.Part.New(name1, units1, out UFPart1);
        Tag[] objarray = new Tag[7];


        Tag wcs, matrix;


        double[] arc1_centerpt = { 0, 0, 0 };
        double arc1_start_ang = 0;

        Double arc1_end_ang = 3.14159265358979324 * 2;

        double arc1_radius = 92.0;

        UFCurve.Arc arc1 = new UFCurve.Arc();
        arc1.start_angle = arc1_start_ang;

        arc1.end_angle = arc1_end_ang;

        arc1.arc_center = new double[3];

        arc1.arc_center[0] = arc1_centerpt[0];

        arc1.arc_center[1] = arc1_centerpt[1];

        arc1.arc_center[2] = arc1_centerpt[2];

        arc1.radius = arc1_radius;

        theUFSession.Csys.AskWcs(out wcs);

        theUFSession.Csys.AskMatrixOfObject(wcs, out matrix);


        arc1.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc1, out objarray[0]);

        double[] direction = { 0.0, 0.0, 1.0 };


        double[] ref_pt = new double[3];

        string taper_angle = "0.0";


        string[] limit = { "0", "8.0" };


        Tag[] f;

        theUFSession.Modl.CreateExtruded(objarray, taper_angle, limit, ref_pt, direction, FeatureSigns.Nullsign, out f);

        Tag[] objarray1 = new Tag[7];


        Tag wcs1, matrix1;


        double[] arc2_centerpt = { 0, 0, 0 };
        double arc2_start_ang = 0;

        Double arc2_end_ang = 3.14159265358979324 * 2;

        double arc2_radius = 50.0;

        UFCurve.Arc arc2 = new UFCurve.Arc();
        arc2.start_angle = arc2_start_ang;

        arc2.end_angle = arc2_end_ang;

        arc2.arc_center = new double[3];

        arc2.arc_center[0] = arc2_centerpt[0];

        arc2.arc_center[1] = arc2_centerpt[1];

        arc2.arc_center[2] = arc2_centerpt[2];

        arc2.radius = arc2_radius;

        theUFSession.Csys.AskWcs(out wcs1);

        theUFSession.Csys.AskMatrixOfObject(wcs1, out matrix1);


        arc2.matrix_tag = matrix;

        theUFSession.Curve.CreateArc(ref arc2, out objarray1[0]);

        double[] direction1 = { 0.0, 0.0, 1.0 };


        double[] ref_pt1 = new double[3];

        string taper_angle1 = "0.0";


        string[] limit1 = { "0", "8.0" };


        Tag[] f1;

        theUFSession.Modl.CreateExtruded(objarray1, taper_angle1, limit1, ref_pt1, direction1, FeatureSigns.Negative, out f1);
    }



    public void Dispose()
    {
        try
        {
            if (isDisposeCalled == false)
            {
                //TODO: Add your application code here 
            }
            isDisposeCalled = true;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
    }

    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }

}

