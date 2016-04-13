package usis;
import java.io.*;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.*;
 
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
 
import org.apache.commons.fileupload.FileItem;
import org.apache.commons.fileupload.FileUploadException;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;
import org.apache.commons.io.output.*;

import com.csvreader.CsvReader;

public class UploadServlet extends HttpServlet {
   
   private boolean isMultipart;
   private String filePath;
   private int maxFileSize = 50 * 1024;
   private int maxMemSize = 4 * 1024;
   private File file ;

   public void init( ){
      // Get the file location where it would be stored.
      filePath = 
             getServletContext().getInitParameter("file-upload"); 
   }
   public void doPost(HttpServletRequest request, 
               HttpServletResponse response)
              throws ServletException, java.io.IOException {
      // Check that we have a file upload request
      isMultipart = ServletFileUpload.isMultipartContent(request);
      response.setContentType("text/html");
      java.io.PrintWriter out = response.getWriter( );
      if( !isMultipart ){
         out.println("<html>");
         out.println("<head>");
         out.println("<title>Servlet upload</title>");  
         out.println("</head>");
         out.println("<body>");
         out.println("<p>No file uploaded</p>"); 
         out.println("</body>");
         out.println("</html>");
         return;
      }
      DiskFileItemFactory factory = new DiskFileItemFactory();
      // maximum size that will be stored in memory
      factory.setSizeThreshold(maxMemSize);
      // Location to save data that is larger than maxMemSize.
      factory.setRepository(new File("D:\\temp"));

      // Create a new file upload handler
      ServletFileUpload upload = new ServletFileUpload(factory);
      // maximum file size to be uploaded.
      upload.setSizeMax( maxFileSize );

      try{ 
      // Parse the request to get file items.
      List fileItems = upload.parseRequest(request);
	
      // Process the uploaded file items
      Iterator i = fileItems.iterator();

      out.println("<html>");
      out.println("<head>");
      out.println("<title>Servlet upload</title>");  
      out.println("</head>");
      out.println("<body>");
      while ( i.hasNext () ) 
      {
         FileItem fi = (FileItem)i.next();
         if ( !fi.isFormField () )	
         {
            // Get the uploaded file parameters
            String fieldName = fi.getFieldName();
            String fileName = fi.getName();
            String contentType = fi.getContentType();
            boolean isInMemory = fi.isInMemory();
            long sizeInBytes = fi.getSize();
            // Write the file
            if( fileName.lastIndexOf("\\") >= 0 ){
               file = new File( filePath + 
               fileName.substring( fileName.lastIndexOf("\\"))) ;
            }else{
               file = new File( filePath + 
               fileName.substring(fileName.lastIndexOf("\\")+1)) ;
            }
            fi.write( file ) ;
            out.println("Uploaded Filename: " + fileName + "<br>");
            
            if(fileName.endsWith(".csv")){
            	out.println("<br/>");
            	out.println("Uploaded data<br/>");
            	out.println("<br/>");
            	try {
        			
        			CsvReader usersReader = new CsvReader("D://temp/"+fileName);
        			
        			usersReader.readHeaders();
        			
        			Connection connection=Util.getConnection();
        			Statement statement=connection.createStatement();
        			Statement updateStatement=connection.createStatement();
        			ResultSet resultSet=statement.executeQuery("select lower(username) username ,password from system_users");
        			Map<String, String> userNameMap=new HashMap<String,String>();
        			while(resultSet.next()){
        				userNameMap.put(resultSet.getString("username").toLowerCase(), "password");
        			}
        			
        			while (usersReader.readRecord())
        			{
        				String username = usersReader.get("username");
        				String password = usersReader.get("password");
        				if(username==null){
        					username="";
        				}
        				if(userNameMap.containsKey(username.toLowerCase())){
        					updateStatement.executeUpdate("update system_users set password='"+password+"' where lower(username)='"+username.toLowerCase()+"'");
        				}else {
        					updateStatement.executeUpdate("insert into system_users(username,password) values('"+username+"','"+password+"')");
        				}
        				
        				// perform program logic here
        				out.println(username + ":" + password+"<br/>");
        			}
        	
        			usersReader.close();
        			
        		} catch (FileNotFoundException e) {
        			e.printStackTrace();
        		} catch (IOException e) {
        			e.printStackTrace();
        		}
            }
         }
      }
      
     
      
      out.println("</body>");
      out.println("</html>");
   }catch(Exception ex) {
       System.out.println(ex);
   }
   }
   public void doGet(HttpServletRequest request, 
                       HttpServletResponse response)
        throws ServletException, java.io.IOException {
	   java.io.PrintWriter out = response.getWriter( );
        out.println("GET method used with " +
                getClass( ).getName( )+": POST method required.");
   } 
}